using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using File = Google.Apis.Drive.v3.Data.File;

// Đổi tên class từ DichVuGoogleBUS thành GoogleDriveService
public class DichVuGoogleBUS
{
    private static string[] Scopes = { DriveService.Scope.DriveFile };
    private static string ApplicationName = "WinForm Whiteboard";

    public static DriveService GetService()
    {
        UserCredential credential;

        using (var stream =
            new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
        }

        return new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
    }

    public static string UploadFile(string filePath)
    {
        var service = GetService();

        var fileMetadata = new File()
        {
            Name = Path.GetFileName(filePath)
        };

        FilesResource.CreateMediaUpload request;

        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            request = service.Files.Create(fileMetadata, stream, "image/png");
            request.Fields = "id";
            request.Upload();
        }

        var file = request.ResponseBody;

        return file.Id; // trả về ID file Drive
    }

    // Bổ sung: Phương thức đặt quyền công khai cho file
    public static void MakeFilePublic(string fileId)
    {
        var service = GetService();

        var newPermission = new Google.Apis.Drive.v3.Data.Permission()
        {
            Type = "anyone",
            Role = "reader"
        };

        try
        {
            service.Permissions.Create(newPermission, fileId).Execute();
        }
        catch (System.Exception ex)
        {
            // Log hoặc xử lý lỗi
            System.Diagnostics.Debug.WriteLine("Error making file public: " + ex.Message);
        }
    }

    // Bổ sung: Phương thức lấy link xem công khai
    public static string GetPublicViewLink(string fileId)
    {
        // Link trực tiếp để xem (dạng web content)
        return $"https://drive.google.com/uc?export=view&id={fileId}";
    }
}