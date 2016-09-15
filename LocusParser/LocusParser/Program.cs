using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LocusParser
{
    class Program
    {
        static List<LocusFrame> lf_list = new List<LocusFrame>();
        static string path;
        static string kml_path;
        static string response;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to binary file:");
            Console.Write(">>");
            path = Console.ReadLine();
            
            /* Check path is valid */
            if(File.Exists(path))
            {
                //Get data from binary file
                using(StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        LocusFrame lf = new LocusFrame();
                        string[] nl = new string[4];
                        byte[] b_lat = new byte[4];
                        byte[] b_lon = new byte[4];
                        nl = sr.ReadLine().Split('\t');

                        b_lat[0]=Convert.ToByte(nl[2].Substring(0,2),16);
                        b_lat[1]=Convert.ToByte(nl[1].Substring(6,2),16);
                        b_lat[2]=Convert.ToByte(nl[1].Substring(4,2),16);
                        b_lat[3]=Convert.ToByte(nl[1].Substring(2,2),16);

                        b_lon[0] = Convert.ToByte(nl[3].Substring(0, 2), 16);
                        b_lon[1] = Convert.ToByte(nl[2].Substring(6, 2), 16);
                        b_lon[2] = Convert.ToByte(nl[2].Substring(4, 2), 16);
                        b_lon[3] = Convert.ToByte(nl[2].Substring(2, 2), 16);

                        lf.UnixTime = Convert.ToUInt32(nl[0], 16);
                        lf.Latitude = BitConverter.ToSingle(b_lat, 0);
                        lf.Longitude = BitConverter.ToSingle(b_lon, 0);
                        lf.Height = 0;

                        //Append element to list
                        lf_list.Add(lf);
                    }
                }
                
                //Create KML file
                kml_path = path.Substring(0, path.Length - 3).ToString() + "kml";

                if (File.Exists(kml_path))
                {
                    Console.Write("File already exists, overwrite? (Y/N) ");
                    response = Console.ReadLine();

                    if (response == "Y" || response == "y")
                    {
                        //Nothing
                    }
                    else
                    {
                        Console.Write("Aborted by user.");
                        Console.ReadKey();
                        return;
                    }
                }

                using (StreamWriter sw = new StreamWriter(kml_path))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sw.WriteLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">");
                    //sw.WriteLine("<kml>\r\n");
                    sw.WriteLine("<Document>\r\n");

                    sw.WriteLine("<Style id=\"Red\">");
                    sw.WriteLine("<LineStyle>");
                    sw.WriteLine("<color>ff0000ff</color>");
                    sw.WriteLine("<width>1.5</width>");
                    sw.WriteLine("</LineStyle>");
                    sw.WriteLine("</Style>\r\n");

                    sw.WriteLine("<Placemark>");
                    sw.WriteLine("<name>Path_001</name>");
                    sw.WriteLine("<styleUrl>#Red</styleUrl>");
                    sw.WriteLine("<LineString>");
                    sw.WriteLine("<coordinates>\r\n");

                    foreach (LocusFrame i in lf_list)
                    {
                        sw.WriteLine(i.Longitude + "," + i.Latitude + "," + i.Height);
                    }
                    sw.WriteLine("\r\n</coordinates>");
                    sw.WriteLine("</LineString>");
                    sw.WriteLine("</Placemark>");
                    sw.WriteLine("</Document>\r\n");

                    sw.WriteLine("</kml>");
                }

                Console.Write("Process complete.");
                Console.ReadKey();
                return;
            }      
        }
    }
}
