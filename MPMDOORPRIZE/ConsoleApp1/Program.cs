using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MailMessage mail = new MailMessage();
                String FromEmail = "autoreply@mpm-motor.com";
                String ToEmail = "fadli.anugrah98@gmail.com";
                mail.From = new MailAddress(FromEmail, "Panitia  MPM End Year Party 2019");
                mail.Subject = "Informasi tambahan mengenai teknis ketentuan souvenir, doorprize, & grandprize";
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("D:\\Project\\Panitia Penutupan\\undangan\\0282.jpeg");
                mail.Attachments.Add(attachment);

                mail.Body = "<!DOCTYPE html>" +
                "<html> " +
                "<head> " +
                "<style> " +
                ".tab {border-collapse: collapse;} " +
                ".tab { border: 1px solid black;} " +
                "</style> " +
                "</head> " +
                "<body> " +
                "<p>Salam Satu Hati,</p> " +
                "<br /> " +
                "<br /> " +
                "<p>Dengan bangga kami mengundang Bpk/Ibu/Sdr. untuk ikut serta menghadiri kegiatan MPM End Year Party 2019 yang diadakan pada : </p> " +
                "<table>" +
                "<tr> " +
                "<td>Hari,tanggal</td>" +
                "<td>:</td> " +
                "<td>Senin, 16 Desember 2019</td> " +
                "</tr> " +
                "<tr> " +
                "<td>Waktu</td> " +
                "<td>:</td> " +
                "<td>pk. 16.30 Wib  – selesai</td> " +
                "</tr> " +
                "<tr> " +
                "<td>Tempat</td>" +
                "<td>:</td> " +
                "<td>Dyandra Convention Center</td> " +
                "</tr> " +
                "<tr> " +
                "<td></td> " +
                "<td></td> " +
                "<td>Jl. Basuki Rahmat 105 Surabaya</td> " +
                "</tr> " +
                "</table> " +
                "<p>Demikian undangan disampaikan, terima kasih atas perhatiannya. </p> " +
                "<p><b>Nb: <br /> 1. Email undangan ini (Barcode) wajib ditunjukkan ke panitia saat registrasi nanti. (bisa dipindahkan ke email yang ada di HP, atau juga bisa di print dulu). <br /> 2. Registrasi bisa dilakukan juga menggunakan QR Code yang ada pada My Tok Apps. <br />3. Undangan tidak bisa dipindahtangankan dan hanya berlaku untuk 1 (satu) orang (khusus karyawan ber-NPK).  </b></p> " +
                "<p><i>Informasi tambahan (ketentuan souvenir, doorprize, & grandprize) :</i></p> " +
                "<table class='tab'> " +
                "<tr class='tab'> " +
                "<th class='tab'>NO.</th> " +
                "<th class='tab'>KETERANGAN</th> " +
                "<th class='tab'>SOUVENIR</th> " +
                "<th class='tab'>DOORPRIZE</th> " +
                "<th class='tab'>GRANDPRIZE</th> " +
                "</tr> " +
                "<tr class='tab'> " +
                "<td class='tab'>1</td> " +
                "<td class='tab'>Karyawan dengan masa kerja <3 bulan </td> " +
                "<td class='tab' style='background-color:grey;text-align: center;'>X</td>" +
                "<td class='tab' style='background-color:grey;text-align: center;'>X</td> " +
                "<td class='tab' style='background-color:grey;text-align: center;'>X</td> " +
                "</tr> " +
                "<tr class='tab'> " +
                "<td class='tab'>2</td> " +
                "<td class='tab'>Karyawan dengan masa kerja >  1 tahun  </td> " +
                "<td class='tab' style='text-align: center;'>V</td> " +
                "<td class='tab' style='background-color:grey;text-align: center;'>X</td> " +
                "<td class='tab' style='background-color:grey;text-align: center;'>X</td> " +
                "</tr> " +
                "<tr class='tab'> " +
                "<td class='tab'>3</td> " +
                "<td class='tab'>Karyawan dengan masa kerja > 1 tahun <br />(posisi div. head down) </td> " +
                "<td class='tab' style='text-align: center;'>V</td> " +
                "<td class='tab' style='text-align: center;'>V</td> " +
                "<td class='tab' style='background-color:grey;text-align: center;'>X</td> " +
                "</tr> " +
                "<tr class='tab'> " +
                "<td class='tab'>4</td> " +
                "<td class='tab'>Karyawan dengan masa kerja > 1 tahun <br />(Grade 1 - 6) " +
                "</td> " +
                "<td class='tab' style='text-align: center;'>V</td> " +
                "<td class='tab' style='text-align: center;'>V</td> " +
                "<td class='tab' style='text-align: center;'>V</td> " +
                "</tr> " +
                "</table> " +
                "<p>Regards,</p> " +
                "<p>Panitia </p> " +
                "<p>CP Panitia : </p> " +
                "<p>Ilham - 082230006776 </p> " +
                "<p> Dony / HRD - 081217175622 </p> " +
                "</body> " +
                "</html>";
                mail.IsBodyHtml = true; // setting true if the email is in HTML format
                mail.To.Add(new MailAddress(ToEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25; // 465 or 587
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("autoreply", "JLBFxUX5");// Your gmail user name and password
                smtp.Host = "mail.mpm-motor.com";// SMTP server
                smtp.Send(mail);


                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
        }
    }
}
