using System.Collections.Generic;
using System.Threading.Tasks;
using BoundfoxStudios.Computermuseum.WebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoundfoxStudios.Computermuseum.WebApi.Data
{
  public class DataInitializer
  {
    private readonly MuseumDbContext _context;

    public DataInitializer(MuseumDbContext context)
    {
      _context = context;
    }

    public async Task InitializeAsync(bool overrideExisting = false)
    {
      var items = new List<MuseumItem>()
      {
        InitializeCommodore()
      };

      foreach (var item in items)
      {
        var existingItem = await _context.MuseumItems.SingleOrDefaultAsync(i => i.IdName == item.IdName);

        if (existingItem != null && overrideExisting)
        {
          _context.Remove(existingItem);
          await _context.SaveChangesAsync();
        }

        if (existingItem == null || overrideExisting)
        {
          await _context.AddAsync(item);
          await _context.SaveChangesAsync();
        }
      }
    }

    // Ripped from Wikipedia :-)
    private MuseumItem InitializeCommodore() =>
      new MuseumItem()
      {
        Name = "1977 PET Commodore 2001",
        IdName = "pet-commodore-2001-1997",
        Description =
          "Produced starting in 1977 by Commodore International. MOS 6502 microprocessor, Commodore BASIC, keyboard, monitor, cassette deck, single all-in-one case.",
        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Commodore_2001_Series-IMG_0448b.jpg",
        InformationPages = new List<InformationPage>()
        {
          new InformationPage()
          {
            Title = "Metainformation",
            Description = @"<size=75%><b>Manufacturer</b></size>
Commodore International

<size=75%><b>Release date</b></size>
January 1977; 43 years ago

<size=75%><b>Operating system</b></size>
Commodore BASIC 1.0 ~ 4.0

<size=75%><b>CPU</b></size>
MOS Technology 6502 @ 1 MHz

<size=75%><b>Memory</b></size>
4–96 KB"
          },
          new InformationPage()
          {
            Title = "Origins",
            Description =
              @"In the 1970s, Commodore was one of many electronics companies selling calculators designed around Dallas-based Texas Instruments (TI) chips. TI faced increasing competition from Japanese vertically-integrated companies who were using new CMOS-based processes and had a lower total cost of production. These companies began to undercut TI business, so TI responded by entering the calculator market directly in 1975. As a result, TI was selling complete calculators at lower price points than they sold just the chipset to their former customers, and the industry that had built up around it was frozen out of the market."
          },
          new InformationPage()
          {
            Title = "Release",
            Description = @"Release
The Commodore PET was officially announced in 1976 and Jack Tramiel gave Chuck Peddle six months to have the computer ready for the January 1977 Consumer Electronics Show, with his team including John Feagans, Bill Seiler, two Japanese engineers named Fujiyama and Aoji, and Jack's son Leonard Tramiel who helped design the PETSCII graphic characters and acted as quality control.

The result was Commodore's first mass-market personal computer, the PET, the first model of which was the PET 2001. Its 6502 processor controlled the screen, keyboard, cassette tape recorders and any peripherals connected to one of the computer's several expansion ports.[8] The PET 2001 included either 4 KB (2001-4) or 8 KB (2001-8) of 8-bit RAM, and was essentially a single-board computer with discrete logic driving a small built-in monochrome monitor with 40×25 character graphics, enclosed in a sheet metal case that reflected Commodore's background as a manufacturer of office equipment.[9] The machine also included a built-in Datasette for data storage located on the front of the case, which left little room for the keyboard. The data transfer rate to cassette tape was 1500 baud, but the data was recorded to tape twice for safety, giving an effective rate of 750 baud.[10] The computer's main board carried four expansion ports: extra memory, a second cassette tape recorder interface, a parallel ('user') port which could be used for sound output or connection to 'user' projects or non-Commodore devices and a parallel IEEE-488 port which allowed for daisy-chaining peripherals such as disk drives and printers."
          }
        }
      };
  }
}
