# Github workflows
# Made by SmallPP420
# the size of this program is gonna be huge xd

ls /home/runner/work/PS4-Firmware-Downloader/

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=WINDOWS --output "build/" --arch x64 --os win -c release --self-contained true # win build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=WINDOWS --output "build-debug/" --arch x64 --os win -c debug --self-contained true # win build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=LINUX --output "build/" --arch x64 --os linux -c release --self-contained true # linux build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=LINUX --output "build-debug/" --arch x64 --os linux -c debug --self-contained true # linux build

# Move debug build
mv "/home/runner/work/PS4-Firmware-Downloader/build-debug/PS4 Firmware Downloader.exe" "/home/runner/work/PS4-Firmware-Downloader/build/DEBUG-PS4 Firmware Downloader.exe"

# Move debug build
mv "/home/runner/work/PS4-Firmware-Downloader/build-debug/PS4 Firmware Downloader" "/home/runner/work/PS4-Firmware-Downloader/build/Linux-DEBUG-PS4 Firmware Downloader"

mv "/home/runner/work/PS4-Firmware-Downloader/build/PS4 Firmware Downloader" "/home/runner/work/PS4-Firmware-Downloader/build/Linux-PS4 Firmware Downloader"
