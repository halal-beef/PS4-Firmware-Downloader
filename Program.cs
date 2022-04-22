using HtmlAgilityPack;
using Spectre.Console;

// Grabbing Locale & Fake Init (lol)
string locale = Thread.CurrentThread.CurrentCulture.Name.ToLower();
AnsiConsole.Markup($"[green]Grabbing Firmware...\n\nDetected Locale: {locale}[/]");
await Task.Delay(5000);
AnsiConsole.Clear();

// Variables
var client = new HttpClient();
HtmlWeb w = new HtmlWeb();
var hd = w.Load($"https://www.playstation.com/{locale}/support/hardware/ps4/system-software/");
string ReinstallLink = "dog fart";
string NormalLink = "donkey fart";
var stuff = hd.DocumentNode.SelectSingleNode(@"//div[@class='accordion__item-description']");
string ChangeLogs = " " + stuff.InnerText.TrimEnd();
var items = new (string name, string url)[1] { ("1","1") };
var linksOnPage = from lnks in hd.DocumentNode.Descendants()
                  where lnks.Name == "a" &&
                       lnks.Attributes["href"] != null &&
                       lnks.InnerText.Trim().Length > 0
                  select new
                  {
                      Url = lnks.Attributes["href"].Value,
                      Text = lnks.InnerText
                  };

// linksOnPage gets all the nodes in the DocumentNode and sees if it has a redirect & if it does it stores it in two strings Url & Text.
// Text is just the text (e.g a if a button had the text "Google" and it redirected to google the Text variable would be Google).
// The Url string stores the Url.
// linksOnPage is an enumerable string

// Main Functions

foreach (var i in linksOnPage)
{

    switch(i.Url)
    {
        case string Rein when Rein.StartsWith("https://pc.ps4.update.playstation.net") && Rein.Contains("rec_"):
            AnsiConsole.Markup("[Green]Grabbed Reinstallation Firmware![/]\n");
            ReinstallLink = Rein;
            break;
        case String Update when Update.StartsWith("https://pc.ps4.update.playstation.net") && Update.Contains("sys_"):
            NormalLink = i.Url;
            AnsiConsole.Markup("[Green]Grabbed Normal Update Firmware![/]\n");
            break;
    }
}

// Iterates through linksOnPage and grabs both firmware links.

AnsiConsole.Markup("[Green]Proceeding To Download Page...[/]");

await Task.Delay(5000);

AnsiConsole.Clear();

// Show Proceding To Download Page message and clear console after 5 seconds

var Choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[Yellow]What Firmware Do You Need??[/]")
        .PageSize(10)
        .AddChoices(new[] {
            "Full Firmware (Recovery)","Normal System Update"
        }));


// Show Our Choices

async Task Download(HttpClient client, ProgressTask task, string url)
{
    try
    {
        using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();

            task.MaxValue(response.Content.Headers.ContentLength ?? 0);
            task.StartTask();

            var filename = @"Downloaded Firmware/" + url.Substring(url.LastIndexOf('/') + 1);
            AnsiConsole.MarkupLine($"Starting download of [u]{filename.Replace(@"Downloaded Firmware/","")}[/] ({task.MaxValue} bytes)");

            using (var contentStream = await response.Content.ReadAsStreamAsync())
            using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                var buffer = new byte[8192];
                while (true)
                {
                    var read = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    if (read == 0)
                    {
                        AnsiConsole.MarkupLine($"Download of [u]{filename.Replace(@"Downloaded Firmware/", "")}[/] [green]completed![/]");
                        break;
                    }

                    task.Increment(read);

                    await fileStream.WriteAsync(buffer, 0, read);
                }
            }
        }
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]Error:[/] {ex}");
    }
}

// Function to download (Uses Spectre Console Progress)

switch (Choice)
{
    case "Full Firmware (Recovery)":
        AnsiConsole.WriteLine(ChangeLogs);
        AnsiConsole.Markup("[Green]Thats all for changelogs :)\nPress Any Key To Continue...[/]");
        Console.ReadKey();
        AnsiConsole.Clear();
        Directory.CreateDirectory("Downloaded Firmware");
        await AnsiConsole.Progress()
    .Columns(new ProgressColumn[]
    {
        new TaskDescriptionColumn(),
        new ProgressBarColumn(),
        new PercentageColumn(),
        new RemainingTimeColumn(),
        new SpinnerColumn(),
    })
    .StartAsync(async ctx =>
    {
        items[0] = ($"Full Recovery Firmware ({ChangeLogs.TrimStart().Split('\n').First()})", ReinstallLink);

        await Task.WhenAll(items.Select(async item =>
        {
            var task = ctx.AddTask(item.name, new ProgressTaskSettings
            {
                AutoStart = false
            });

            await Download(client, task, item.url);
        }));
    });
        break;  
    case "Normal System Update":
        AnsiConsole.WriteLine(ChangeLogs);
        AnsiConsole.Markup("[Green]Thats all for changelogs :)\nPress Any Key To Continue...[/]");
        Console.ReadKey();
        Directory.CreateDirectory("Downloaded Firmware");
        AnsiConsole.Clear();
        await AnsiConsole.Progress()
    .Columns(new ProgressColumn[]
    {
        new TaskDescriptionColumn(),
        new ProgressBarColumn(),
        new PercentageColumn(),
        new RemainingTimeColumn(),
        new SpinnerColumn(),
    })
    .StartAsync(async ctx =>
    {
        items[0] = ($"Normal Firmware ({ChangeLogs.TrimStart().Split('\n').First()})", NormalLink);

        await Task.WhenAll(items.Select(async item =>
        {
            var task = ctx.AddTask(item.name, new ProgressTaskSettings
            {
                AutoStart = false
            });

            await Download(client, task, item.url);
        }));
    });

        break;
}

// See which choice user selected and download the firmware user wanted.

Thread.Sleep(-1);

// This will make console not auto close :D
