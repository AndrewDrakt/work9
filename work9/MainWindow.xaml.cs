using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using (var client = new ImapClient())
{
    client.Connect("imap.mail.server", 993, true);

    // Note: since we don't have an OAuth2 token, disable
    // the XOAUTH2 authentication mechanism.
    client.AuthenticationMechanisms.Remove("XOAUTH2");

    client.Authenticate("username", "password");

    // The Inbox folder is always available on all IMAP servers...
    var inbox = client.Inbox;
    inbox.Open(FolderAccess.ReadOnly);

    Console.WriteLine("Total messages: {0}", inbox.Count);
    Console.WriteLine("Recent messages: {0}", inbox.Recent);

    for (int i = 0; i < inbox.Count; i++)
    {
        var message = inbox.GetMessage(i);
        Console.WriteLine("Subject: {0}", message.Subject);
    }

    client.Disconnect(true);
}

namespace work9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    }
}
