## NovaHQ PFF Extractor / Editor

Visit [https://novahq.net](https://novahq.net) for more information on NovaLogic file formats and tools to work with these files. 

**Related research notes**:

- [NovaLogic File Formats](https://github.com/Novahq-net/NovaResearch/)

### NovaPff
A GUI tool for reading, writing, and creating NovaLogic PFF archives built on top of [NHQTools](https://github.com/Novahq-net/NHQTools). It supports all variants (PFF0-PFF4) and handles their specific quirks like alignment and CRC calculations. Instead of just importing and exporting files, NovaPff lets you interact with the contents directly:

* Preview textures
* Sample audio files
* Edit text-based formats directly inside the archive
* Save decrypted files for manual editing and re-import
* Optimize archives by cleaning up dead-space

![NovaPff](/NovaPff/Resources/NovaPff.png)

### Supported PFF Variants
**Note:** Some games have multiple PFF variants. When modifying a PFF file, make sure you match the original PFF format for the particular archive you are working with.

| Format | Entry&nbsp;Size | Games |
|--------|------------|-------|
| PFF0 | 32 | `F-22 Raptor (Non-IBS)` |
| PFF2 | 32 | `Comanche 3 Gold` |
| PFF3 | 32 | `Armored Fist 3`, `F16 Multirole Fighter`, `F-22 Raptor IBS`, `F-22 Lightning 3`, `MiG29 Fulcrum`, `Tachyon: The Fringe`, `Delta Force 1`, `Delta Force 2` |
| PFF3 | 36 | `Comanche 4`, `Land Warrior`, `Task Force Dagger`, `Black Hawk Down / Team Sabre`, `Joint Operations`, `Delta Force Xtreme` |
| PFF4 | 36 | `Delta Force Xtreme 2` |

---

## Usage

1. Make PFF backups
2. Open `NovaPff.exe`.
3. Click stuff.

**WARNING:** If your game is installed in the **Program Files** folder, you will need to run NovaPff **as administrator** to have write access to the game files.  

To **avoid this requirement**, I recommend installing all NovaLogic games **outside of the Program Files** folder. Someplace like `C:\Users\<YourUsername>\Games` is a better option as you will have full read/write access without needing to run as administrator.  

**This is the #1 cause of issues for most users trying to modify their game files.**  

There is **no need** to run this app as administrator if the files you want to modify are located in a directory with write permissions for your user account. Your user account,
even if it is an administrator account, does not have write permissions to files in Program Files by default. 
This is a security feature of Windows to prevent unauthorized modifications to important system files and applications. **This is not an issue with the app itself.**

If you modify a PFF or other game file in the Program Files directory without admin permissions, the app will most likey save files to the **Virtual Store** 
instead of the actual game directory, which can cause confusion when trying to find your modified files. The modified files will also not properly load into the game if one of the original files is still present in the game directory, as the game will read the original file instead of the modified one in the Virtual Store.  

**The Virtual Store for Windows Vista+ is located at** `C:\Users\<YourUsername>\AppData\Local\VirtualStore` or `%LocalAppData%\VirtualStore`. Visit [Virtual Store explanation from Microsoft Q&A](https://learn.microsoft.com/en-us/answers/questions/2639269/please-explain-virtualstore-for-non-experts)

---

## Building from Source

### Requirements
- Windows 10+
- [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
- Visual Studio 2022 or newer (recommended for designer support)

### Steps

1. **Clone the repository with submodules**
   ```
   git clone --recurse-submodules https://github.com/Novahq-net/NovaPff
   ```
   If you already cloned without `--recurse-submodules`, run:
   ```
   git submodule update --init --recursive
   ```

2. **Open the solution**
   Open `NovaPff.sln` in Visual Studio 2022 or newer.

3. **Set build configuration**
   In the toolbar set the configuration to `Release` and platform to `Any CPU`.

4. **Build the solution**
   Go to **Build → Build Solution** (or press `Ctrl+Shift+B`) to generate `NHQTools.dll` and `NovaPff.exe`.

5. **Output**
   The built files will be located in `NovaPff\bin\Release\`.
