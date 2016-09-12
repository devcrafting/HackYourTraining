@echo off

if not exist .paket\paket.exe (
    @echo "Downloading Paket"
    .paket\paket.bootstrapper.exe
)

@echo "Restoring dependencies"
.paket\paket.exe restore

@echo "Installing npm dependencies"
npm install

@echo "Build server"
.\fake.cmd