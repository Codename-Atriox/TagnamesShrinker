using System.IO;
using System.Text;

namespace TagnamesShrinker
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            Dictionary<int, string> tagnames = new();

            using (var reader = new StreamReader("C:\\Users\\Joe bingle\\Downloads\\IRTV\\files\\tagnames.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split(" : ");

                    int id = Convert.ToInt32(split[0], 16);
                    if (!tagnames.ContainsKey(id)){ // then add it
                        string name = split[1]; //.Split("\\").Last(); // incase we only need the filename & not the entire path
                        tagnames.Add(id, name);
                    }
                }
            }

            // then convert it all to a massive byte array to write to
            using (var fs = new FileStream("C:\\Users\\Joe bingle\\Downloads\\IRTV\\files\\tagnamesSLIM.txt", FileMode.Create, FileAccess.Write)){
                foreach (var v in tagnames){
                    fs.Write(BitConverter.GetBytes(v.Key).Reverse().ToArray());
                    fs.Write(Encoding.UTF8.GetBytes(v.Value + '\0'));
                }
            }
        }
    }
}