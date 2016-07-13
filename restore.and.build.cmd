@echo off

if not exist .paket\paket.exe (
    @echo "Downloading Paket"
    .paket\paket.bootstrapper.exe
)

@echo "Restoring dependencies"
.paket\paket.exe restore

rem Temporary until PR https://github.com/fsprojects/Fable/pull/256 has been accepted and Node package deployed
if not exist packages/Fable (
    git clone https://github.com/devcrafting/Fable packages/Fable
)

@echo "Build server"
.\fake.cmd