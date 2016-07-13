#!/bin/bash

if [ ! -f ".paket/paket.exe" ]; then
  echo "Downloading Paket"
  mono .paket/paket.bootstrapper.exe
fi

echo "Restoring dependencies"
mono .paket/paket.exe restore

# Temporary until PR https://github.com/fsprojects/Fable/pull/256 has been accepted and Node package deployed
if [ ! -d "packages/Fable" ]; then
  git clone https://github.com/devcrafting/Fable packages/Fable
fi

echo "Build server"
sh ./fake.sh $@