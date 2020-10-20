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
          "The Commodore PET is a line of home/personal computers produced starting in 1977 by Commodore International. The system combined a MOS 6502 microprocessor, Commodore BASIC in read only memory (ROM), a keyboard, a computer monitor and (in early models) a cassette deck for data and program storage in a single all-in-one case.",
        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Commodore_2001_Series-IMG_0448b.jpg",
        InformationPages = new List<InformationPage>()
        {
          new InformationPage()
          {
            Title = "Metainformation",
            Description = @"<b>Manufacturer</b> Commodore International
<b>Type</b>	Personal computer
<b>Release date</b>	January 1977; 43 years ago
<b>Introductory price</b>	US$795 (equivalent to $3,354 in 2019)
<b>Discontinued</b>	1982; 38 years ago
<b>Operating system</b> Commodore BASIC 1.0 ~ 4.0
<b>CPU</b> MOS Technology 6502 @ 1 MHz
<b>Memory</b> 4–96 KB
<b>Storage</b> cassette tape, 5.25-inch floppy, 8-inch floppy, hard disk
<b>Display</b> 40×25 or 80×25 text
<b>Graphics</b> monochrome character graphics
<b>Sound</b> none or beeper"
          },
          new InformationPage()
          {
            Title = "Origins",
            Description =
              @"In the 1970s, Commodore was one of many electronics companies selling calculators designed around Dallas-based Texas Instruments (TI) chips. TI faced increasing competition from Japanese vertically-integrated companies who were using new CMOS-based processes and had a lower total cost of production. These companies began to undercut TI business, so TI responded by entering the calculator market directly in 1975. As a result, TI was selling complete calculators at lower price points than they sold just the chipset to their former customers, and the industry that had built up around it was frozen out of the market.

Commodore initially responded by beginning their own attempt to form a vertically-integrated calculator line as well, purchasing a vendor in California that was working on a competitive CMOS calculator chip and an LED production line. They also went looking for a company with an existing calculator chip line, something to tide them over in the immediate term, and this led them to MOS Technology. MOS had been building calculator chips for some time, but more recently had begun to branch out into new markets with its 6502 microprocessor design, which they were trying to bring to market.

Along with the 6502 came Chuck Peddle's KIM-1 design, a small computer kit based on the 6502. At Commodore, Peddle convinced Jack Tramiel that calculators were a dead-end and that Commodore should explore the burgeoning microcomputer market instead. At first, they considered purchasing an existing design, and in September 1976 Peddle got a demonstration of Jobs and Wozniak's Apple II prototype. Jobs was offering to sell it to Commodore, but Commodore considered Jobs's offer too expensive."
          },
          new InformationPage()
          {
            Title = "Release",
            Description = @"Release
The Commodore PET was officially announced in 1976 and Jack Tramiel gave Chuck Peddle six months to have the computer ready for the January 1977 Consumer Electronics Show, with his team including John Feagans, Bill Seiler, two Japanese engineers named Fujiyama and Aoji, and Jack's son Leonard Tramiel who helped design the PETSCII graphic characters and acted as quality control.[7]

The result was Commodore's first mass-market personal computer, the PET, the first model of which was the PET 2001. Its 6502 processor controlled the screen, keyboard, cassette tape recorders and any peripherals connected to one of the computer's several expansion ports.[8] The PET 2001 included either 4 KB (2001-4) or 8 KB (2001-8) of 8-bit RAM, and was essentially a single-board computer with discrete logic driving a small built-in monochrome monitor with 40×25 character graphics, enclosed in a sheet metal case that reflected Commodore's background as a manufacturer of office equipment.[9] The machine also included a built-in Datasette for data storage located on the front of the case, which left little room for the keyboard. The data transfer rate to cassette tape was 1500 baud, but the data was recorded to tape twice for safety, giving an effective rate of 750 baud.[10] The computer's main board carried four expansion ports: extra memory, a second cassette tape recorder interface, a parallel ('user') port which could be used for sound output or connection to 'user' projects or non-Commodore devices and a parallel IEEE-488 port which allowed for daisy-chaining peripherals such as disk drives and printers.[11]

A working PET 2001 prototype with wooden case was shown to the public at the Winter CES 1977 [4] in January 1977 and the first 100 units were shipped in October, mostly going to magazines and software developers, while the machine was not generally available to consumers until December.[12] However, the PET was back-ordered for months and to ease deliveries, early in 1978 Commodore decided to cancel the 4 KB version (also because the user would be left with barely 3 KB of RAM).[13]

Dan Fylstra of Byte Magazine received one of the initial PETs in October 1977, S/N 16, and reported on the computer in the March 1978 issue. Fylstra praised its full-featured BASIC, lowercase letters, and reliable cassette system, while disapproving of the keyboard. His machine had three faulty RAM chips and after some difficulty contacting Commodore, was mailed a set of replacement chips and installation instructions by John Feagans.[14]

Commodore was the first company to license Microsoft's 6502 BASIC, but the agreement nearly drove Microsoft into receivership as Commodore stipulated that they would only pay for it when the PET began shipping. This was delayed by over six months, during which Microsoft lost money and had their cash reserves further depleted by a lawsuit over ownership of Altair BASIC. At the end of the year, Microsoft was saved by Apple's decision to license Microsoft BASIC for the Apple II line.

The BASIC included on the original PET 2001 was known as Commodore BASIC 1.0; Microsoft supplied Commodore with a source listing for their 6502 BASIC, essentially a port of BASIC-80, and Commodore performed the rest of the work themselves, including changing the startup screen and prompts, adding I/O support, the SYS command for invoking machine language programs, and fixing bugs. BASIC 1.0 still had numerous bugs and IEEE-488 support was broken, so that when Commodore later came out with disk drives, they could not be used from BASIC (although the kernel routines supported them), and only supported 256 array elements. The PEEK function would not work on memory locations above 49152 so as to prevent the user from viewing the copyrighted code in the system ROMs.

Aside from the 8k BASIC ROM, the PET also included a 4k character ROM and an 8k kernal ROM. The first half of the kernal contained screen editor functions (the screen editor on 80 column PETs differed from 40-column models) with the second half containing a number of function calls for tasks such as inputting and outputting data to and from different I/O devices, reading the keyboard, and positioning the cursor. In addition, the kernal ROM received system interrupts and scanned the keyboard. The kernel, an idea of John Feagans, was a spiritual ancestor to the ROM BIOS on PC compatibles and the first personal computer OS ROM to be a distinct entity from BASIC. The character ROM was 4k in size, containing four different 128 character tables, the uppercase/graphics character set and upper/lowercase character set, plus reverse video versions of both. This included a number of graphics characters for creating pseudographics on the screen as well as playing card symbols (reportedly because Jack Tramiel's sons wanted to play card games on the computer). On the original PET 2001, the uppercase/graphics character set and upper/lowercase character set were reversed compared to how they would be on later machines; PET owners who upgraded their machines to the BASIC 2.0 ROMs often also swapped out the character ROMs for the newer version.[15]


The Commodore PET 2001-8 alongside its rivals: The Apple II and the TRS-80 Model I
Although the machine was moderately successful, there were frequent complaints about the tiny calculator-like keyboard, often referred to as a 'chiclet keyboard' because the keys resembled the chewing gum it was named after. The key tops also tended to rub off easily. Reliability was fairly poor, although that was not atypical of many early microcomputers. Because of the poor keyboard on the PET, external replacement ones quickly appeared.[citation needed] The PET had somewhat of a competitive advantage over its Apple II and TRS-80 rivals as both were using relatively primitive integer BASICs for their first six months on the market while the PET had a full-featured BASIC with floating point support, a sophisticated screen editor, and lowercase letters, the last being a feature that the two competing platforms would not have for a few years. On the other hand, Commodore were a year behind Apple and Tandy in making disk drives available for their computers.

In 1979, Commodore replaced the original PET 2001 with an improved model known as the 2001-N (the N was short for 'New'). The new machine used a standard green-phosphor monitor in place of the white in the original 2001. It now had a conventional, full-sized keyboard and no longer sported the built-in cassette recorder. The kernel ROM was upgraded to add support for Commodore's newly-introduced disk drive line. It was offered in 8 KB, 16 KB, or 32 KB models as the 2001-N8, 2001-N16, and 2001-N32 (the 8 KB models were dropped soon after introduction). The 2001-N switched to using conventional DRAM instead of the 6550 (1kx4) SRAM in the original model. PET 2001-8Ns had eight 2108 (8kx1) DRAMs and 2001-16Ns used sixteen 2108s. The PET 4016 used eight 4116 (16kx1) chips. All 32k PETs used sixteen 4116 chips. Finally, Commodore added a machine-language monitor to the kernel ROM that could be accessed by jumping to any memory location with a BRK instruction. It did not include a built-in assembler and required the user to enter hexadecimal numbers for coding.

Commodore contacted Microsoft to upgrade BASIC for the new machines; this resulted in the soon-to-be-familiar BASIC 2.0 which removed the 256 element array limitation and had a rearranged zero page. Most bugs were fixed and IEEE-488 support in BASIC was made to be functional. The PEEK function was unblocked for memory locations above 49152. BASIC 2.0 also included an easter egg that Bill Gates personally coded, which would cause 'MICROSOFT!' to appear if the user typed WAIT 6502,x (x being the number of times to display the message); this was reportedly due to a dispute with Commodore over ownership of BASIC (years later, when Microsoft developed BASIC for the Amiga, one of their conditions was that Commodore credit the original authors of BASIC, so BASIC 7.0 on the Commodore 128 displayed a Microsoft copyright notice). This feature was present in all 30xx series PETs. Commodore executives were unhappy when they learned about it and it was removed from BASIC on all subsequent Commodore machines. Microsoft also remained sensitive about their copyrighted code and pressured Commodore to not release any BASIC code listings to the public, although user groups eventually made disassemblies of BASIC.

Sales of the newer machines were strong, and Commodore then introduced the models to Europe. However, Philips owned a competing trademark on the PET name, so these models were renamed. The result was the CBM 3000 series ('CBM' standing for Commodore Business Machines), which included the 3008, 3016 and 3032 models. Like the 2001-N-8, the 3008 was quickly dropped. Later PET 3000 series machines switched to the BASIC 4.0 ROMs."
          }
        }
      };
  }
}
