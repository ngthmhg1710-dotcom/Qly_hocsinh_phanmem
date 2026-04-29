// THAY THẾ TOÀN BỘ FILE CŨ BẰNG FILE NÀY

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DTO;

namespace FlashcardFlipGame
{
    public partial class FlashcardControl : Control
    {
        // MỚI: Event để thông báo cho Form cha khi thẻ được lật
        public event EventHandler CardFlipped;

        // --- Properties ---
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public Color FrontFaceColor { get; set; } = Color.FromArgb(255, 248, 227);
        public Color BackFaceColor { get; set; } = Color.FromArgb(215, 237, 250);
        public int CornerRadius { get; set; } = 20;
        private Color _backTextColor = Color.Black;
        public Color BackTextColor
        {
            get { return _backTextColor; }
            set { _backTextColor = value; Invalidate(); }
        }

        private Image frontImage;
        private Image backImage;

        private Timer animationTimer;
        private DateTime animationStartTime;
        private readonly int animationDurationMs = 500;
        private int animationDirection = 0;
        private float currentFlipAngle = 0f;

        public bool IsFrontShowing => currentFlipAngle < 0.5f;

        public FlashcardControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            this.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            animationTimer = new Timer { Interval = 10 };
            animationTimer.Tick += AnimationTimer_Tick;
            this.Click += (s, e) => Flip();
        }

        #region Public Methods
        public void SetCard(Flashcard card)
        {
            frontImage?.Dispose();
            backImage?.Dispose();
            frontImage = null;
            backImage = null;

            FrontText = card.FrontText;
            BackText = card.BackText;

            if (!string.IsNullOrEmpty(card.FrontImagePath))
            {
                try { frontImage = Image.FromFile(card.FrontImagePath); }
                catch (Exception ex) { MessageBox.Show($"Không thể tải ảnh mặt trước:\n{ex.Message}"); }
            }
            if (!string.IsNullOrEmpty(card.BackImagePath))
            {
                try { backImage = Image.FromFile(card.BackImagePath); }
                catch (Exception ex) { MessageBox.Show($"Không thể tải ảnh mặt sau:\n{ex.Message}"); }
            }
            ResetToFront();
        }

        public void Flip()
        {
            if (animationDirection != 0) return;

            // MỚI: Kích hoạt event khi bắt đầu lật thẻ
            CardFlipped?.Invoke(this, EventArgs.Empty);

            animationDirection = IsFrontShowing ? 1 : -1;
            animationStartTime = DateTime.Now;
            animationTimer.Start();
        }

        public void ResetToFront()
        {
            animationDirection = 0;
            currentFlipAngle = 0f;
            animationTimer.Stop();
            Invalidate();
        }
        #endregion

        // ... (Tất cả các hàm khác từ Animation Logic đến Dispose giữ nguyên không đổi) ...
        #region Animation Logic
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            var elapsed = (float)(DateTime.Now - animationStartTime).TotalMilliseconds;
            float progress = Math.Min(elapsed / animationDurationMs, 1.0f);
            float easedProgress = EasingFunctions.EaseInOutCubic(progress);

            if (animationDirection == 1)
                currentFlipAngle = easedProgress;
            else
                currentFlipAngle = 1.0f - easedProgress;

            if (progress >= 1.0f)
            {
                animationDirection = 0;
                animationTimer.Stop();
                currentFlipAngle = (currentFlipAngle > 0.5f) ? 1.0f : 0.0f;
            }
            this.Invalidate();
        }
        #endregion

        #region Custom Painting
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            float angleRadian = currentFlipAngle * (float)Math.PI;
            float scale = (float)Math.Cos(angleRadian);
            float absScale = Math.Abs(scale);

            bool isDrawingFront = currentFlipAngle < 0.5f;
            string text = isDrawingFront ? FrontText : BackText;
            Image image = isDrawingFront ? frontImage : backImage;
            Color bgColor = isDrawingFront ? FrontFaceColor : BackFaceColor;
            Color textColor = isDrawingFront ? this.ForeColor : this.BackTextColor;
            int currentHeight = (int)(this.Height * absScale);
            if (currentHeight <= 2) return;
            int yOffset = (this.Height - currentHeight) / 2;
            var cardRect = new Rectangle(0, yOffset, this.Width, currentHeight);

            using (var path = GetRoundedRect(cardRect, CornerRadius))
            using (var brush = new SolidBrush(bgColor))
                g.FillPath(brush, path);

            DrawCardContent(g, cardRect, text, image, textColor, absScale);

            float penWidth = 2f;
            var borderRect = cardRect;
            borderRect.Inflate(-(int)penWidth, -(int)penWidth);
            int borderCornerRadius = CornerRadius - (int)penWidth;
            if (borderCornerRadius < 1) borderCornerRadius = 1;

            using (var path = GetRoundedRect(borderRect, borderCornerRadius))
            using (var pen = new Pen(textColor, penWidth))
                g.DrawPath(pen, path);
        }

        private void DrawCardContent(Graphics g, Rectangle cardRect, string text, Image image, Color textColor, float scale)
        {
            Rectangle contentRect = new Rectangle(
                cardRect.X + CornerRadius,
                cardRect.Y + CornerRadius / 2,
                cardRect.Width - CornerRadius * 2,
                cardRect.Height - CornerRadius);

            if (contentRect.Width <= 0 || contentRect.Height <= 0) return;

            if (image != null && string.IsNullOrEmpty(text))
            {
                DrawRoundedImage(g, image, contentRect, CornerRadius - 5);
            }
            else if (image == null && !string.IsNullOrEmpty(text))
            {
                DrawText(g, text, textColor, contentRect, scale);
            }
            else if (image != null && !string.IsNullOrEmpty(text))
            {
                var imageRect = new Rectangle(
                    contentRect.X, contentRect.Y,
                    contentRect.Width, (int)(contentRect.Height * 0.7f));
                var textRectangle = new Rectangle(
                    contentRect.X, contentRect.Y + imageRect.Height,
                    contentRect.Width, (int)(contentRect.Height * 0.3f));
                DrawRoundedImage(g, image, imageRect, CornerRadius - 5);
                DrawText(g, text, textColor, textRectangle, scale);
            }
        }

        private void DrawRoundedImage(Graphics g, Image image, Rectangle bounds, int cornerRadius)
        {
            if (image == null || bounds.Width <= 0 || bounds.Height <= 0) return;
            RectangleF targetRect = CalculateAspectRatioFit(image.Size, bounds);
            using (var imagePath = GetRoundedRect(Rectangle.Round(targetRect), cornerRadius))
            {
                var oldClip = g.Clip;
                g.SetClip(imagePath, CombineMode.Replace);
                g.DrawImage(image, targetRect);
                g.Clip = oldClip;
            }
        }

        private RectangleF CalculateAspectRatioFit(SizeF imageSize, RectangleF bounds)
        {
            if (imageSize.Height == 0 || bounds.Height == 0) return bounds;
            float imageRatio = imageSize.Width / imageSize.Height;
            float boundsRatio = bounds.Width / bounds.Height;
            float newWidth, newHeight;
            if (imageRatio > boundsRatio)
            {
                newWidth = bounds.Width;
                newHeight = newWidth / imageRatio;
            }
            else
            {
                newHeight = bounds.Height;
                newWidth = newHeight * imageRatio;
            }
            float x = bounds.X + (bounds.Width - newWidth) / 2;
            float y = bounds.Y + (bounds.Height - newHeight) / 2;
            return new RectangleF(x, y, newWidth, newHeight);
        }

        private void DrawText(Graphics g, string text, Color color, RectangleF rect, float scale)
        {
            if (string.IsNullOrEmpty(text) || rect.Height < 10) return;
            int textAlpha = (int)(255 * Math.Pow(scale, 1.5));
            textAlpha = Math.Max(0, Math.Min(255, textAlpha));
            using (var brush = new SolidBrush(Color.FromArgb(textAlpha, color)))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                float fontSize = this.Font.Size;
                SizeF textSize = g.MeasureString(text, this.Font);
                if (textSize.Width > rect.Width && textSize.Width > 0)
                {
                    fontSize = (rect.Width / textSize.Width) * fontSize;
                }
                using (var tempFont = new Font(this.Font.FontFamily, fontSize, this.Font.Style))
                {
                    g.DrawString(text, tempFont, brush, rect, sf);
                }
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0) { path.AddRectangle(bounds); return path; }
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Dispose();
                frontImage?.Dispose();
                backImage?.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}