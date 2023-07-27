This tool combines two versions of the data.dat file used by the [Solgryn's Pokémon On Stream](https://www.grynsoft.com/spos-app/) app.

## Instructions:

1. Make sure the Solgryn app is closed

2. Download the release .zip and extract it somewhere

2. Find the data.dat file for the solgryn app in your AppData path like below:
`C:\Users\your-username\AppData\LocalLow\Grynsoft\Solgryns Pokemon On Twitch`

3. Take the newer copy of the file, make a copy called "data-new.dat", and put it where you extracted the zip

4. Take the older copy of the file, make a copy and call it "data-old.dat", and put it where you extracted the zip

5. Open a command prompt to the extracted release and run this command (Change steamerusername to the username of the Twitch channel)
`Assembly-CSharp.exe steamerusername data-old.dat data-new.dat`

6. The files should be combined into data-merged.dat. Use this file to overwrite your original data.dat and you're done!
