# NetCheat-PS4RPC
Implementation of jkPatch RPC with NetCheat



# Installing
Download the latest [release file](https://github.com/iCyb3r/NetCheat-PS4RPC/releases), extract it and drop the dll into your APIs folder



# How to Use
* Fire up your game
* After starting the game load the exploit `Original` through your browser and leave the browser open
* Open `NetCheat 64bit` and change the API to `PS4 RPC` (check the notes below if you get an error)
* Click `Connect`, a pop-up will appear choose your FW version & change IP to your PS4 IP address then click `Inject Payload`
* If the payload get injected successfully click `Connect`
* Click attach and and choose a memory region to work with



# Notes & Hints
* If you have `PS4API-NC.dll` in your `APIs` folder you have to move it somewhere else it has a conflict for unknown reason
* After selecting a memory region the range addresses will be inserted in `Start`/`Stop` fields without any interaction on your end (the ranges are not real it's just an alias because netcheat is clunky when working with 64bit addresses so I had to make them shorter)
* When you select a memory region the start address will always be the same `0x0`, this is useful if you want to modify the `ELF` region without worrying about ASLR (the `ELF` region is usually the first one named executable)
* If you want to choose another region just click on the play button (Continue button) This also applies to changing games, you don't have to disconnect and connect again just click the same button after starting your game
