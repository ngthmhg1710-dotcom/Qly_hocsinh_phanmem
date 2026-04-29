// File: EasingFunctions.cs

using System;

namespace FlashcardFlipGame
{
    /// <summary>
    /// Chứa các hàm toán học để tạo hiệu ứng animation mượt mà (Ease-in, Ease-out).
    /// </summary>
    public static class EasingFunctions
    {
        // Hàm EaseInOutCubic: Bắt đầu chậm, tăng tốc ở giữa, kết thúc chậm.
        public static float EaseInOutCubic(float t)
        {
            return t < 0.5f
                ? 4f * t * t * t
                : 1f - (float)Math.Pow(-2f * t + 2f, 3) / 2f;
        }
    }
}