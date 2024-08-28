using System;
using System.Collections.Generic;

using System.Drawing.Drawing2D;

using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace Vera.Infrastructure {

    /*Мне удалось удалить предупреждения CA1416, добавив следующий декоратор в начало содержащего класса:*/
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class CaptchaImage {

        private string text; // текст капчи
        private int width; // ширина картинки
        private int height; // высота картинки
        public Bitmap Image { get; set; } = null!;// само изображение капчи

        //--------------------------------------------
        public CaptchaImage(string s, int width, int height) {
            text = s;
            this.width = width;
            this.height = height;

            GenerateImage();
        }
        //--------------------------------------------
        //--------------------------------------------

        // создаем изображение
        private void GenerateImage() {

            Bitmap bitmap = new(width, height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);

            // отрисовка строки
            g.DrawString(text, new Font("Arial", height / 2, FontStyle.Bold),
                                Brushes.Red, new RectangleF(0, 0, width, height));

            g.Dispose();

            Image = bitmap;
        }
        //--------------------------------------------
        ~CaptchaImage() {

            Dispose(false);

        }
        //--------------------------------------------
        public void Dispose() {

            GC.SuppressFinalize(this);
            Dispose(true);

        }
        //--------------------------------------------
        protected virtual void Dispose(bool disposing) {

            if (disposing)
                Image.Dispose();

        }
        //--------------------------------------------
        //--------------------------------------------
    }
}