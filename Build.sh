# Github workflows
# Made by SmallPP420
# the size of this program is gonna be huge xd

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=WINDOWS --output "build/" --arch x64 --os win -c release --self-contained true # win build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=WINDOWS --output "build-debug/" --arch x64 --os win -c debug --self-contained true # win build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=LINUX --output "build/" --arch x64 --os linux -c release --self-contained true # linux build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=LINUX --output "build-debug/" --arch x64 --os linux -c debug --self-contained true # linux build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=OSX --output "build-osx/" --arch x64 --os osx -c release --self-contained true # MacOS build

dotnet publish "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/PS4 Firmware Downloader.csproj" /p:DefineConstants=OSX --output "build-debug-osx/" --arch x64 --os osx -c debug --self-contained true # MacOS build

# Move debug build
mv "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build-debug/PS4 Firmware Downloader.exe" "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build/DEBUG-PS4 Firmware Downloader.exe"

mv "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build-debug/PS4 Firmware Downloader" "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build/Linux-DEBUG-PS4 Firmware Downloader"

mv "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build/PS4 Firmware Downloader" "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build/Linux-PS4 Firmware Downloader"

mv "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build-debug-osx/PS4 Firmware Downloader" "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build/MacOS-DEBUG-PS4 Firmware Downloader"

mv "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build-osx/PS4 Firmware Downloader" "/home/runner/work/PS4-Firmware-Downloader/PS4-Firmware-Downloader/build/MacOS-PS4 Firmware Downloader"
