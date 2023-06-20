namespace PhotoBookClass
{
    public class PhotoBookTest
    {
        static void Main(string[] arg)
        {
            PhotoBook myAlbum = new PhotoBook();
            Console.WriteLine(myAlbum.GetNumberPages());

            PhotoBook myArtist = new PhotoBook(24);
            Console.WriteLine(myArtist.GetNumberPages());

            BigPhotoBook myBigAlbum = new BigPhotoBook();
            Console.WriteLine(myBigAlbum.GetNumberPages());
        }
    }
}
