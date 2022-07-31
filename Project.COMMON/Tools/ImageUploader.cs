using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.COMMON.Tools
{
    public static class ImageUploader
    {
        //Geriye string değer döndüren metodumuz resmin yolunu döndürecek veya resim yükleme ile ilgili bir sorun varsa onun kodunu döndürecek "1","2","3","C:/Images/"

        //HttpPostedFileBase => MVC'de eğer post yönteminde bir dosya geliyorsa bu dosya, HttpPostedFileBase tipinde tutulur...

        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();

                string[] fileArray = file.FileName.Split('.'); //burada Split metodu sayesinde ilgili yapının uzantısının da içeride bulunduğu bir string dizisi almış olduk...Split metodu belirttiğimiz char karakterinden metni bölerek bize bir array sunar...

                string extension = fileArray[fileArray.Length - 1].ToLower(); //Dosya uzantısını yakalayarak küçük harflere çevirdik...

                string fileName = $"{uniqueName}.{extension}"; //Normal şartlarda burada Guid kullandığımız için asla bir dosya ismi aynı olmayacaktır...Lakin Guid kullanmazsanız (kullanıcıya yüklemek istediği dosyanın ismini girdirebilirsiniz)...Böyle bir durum söz konusu ise ek olarak bir kontrol yapmanız gerekir...Bunu öncelikle extension'ı kontrol edip sonra yapmalısınız...

                if (extension == "jpg" || extension == "gif" || extension == "png" || extension == "jpeg")
                {
                    //Eğer dosya ismi zaten varsa
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "1"; //Guid kullandığımız için bu açıdan güvendeyiz...(Dosya zaten var kodu)
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }
                }
                else
                {
                    return "2"; //Seçilen dosya bir resim değildir...
                }
            }
            else
            {
                return "3"; //Dosya boş kodu
            }
        }
        
    }
}
