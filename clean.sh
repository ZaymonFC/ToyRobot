#! /usr/bin/env bash

if [ $BASH_VERSINFO -lt 4 ];
then echo "Please upgrade to a bash version > 4.2.x \n\n > 'brew install bash'\n"; exit -1;
fi

echo 'Cleaning Directory:'

echo -e '\tDeleting | bin'
find . -type d -name 'bin' -exec rm -r {} +

echo -e '\tDeleting | obj'
find . -type d -name 'obj' -exec rm -r {} +

echo -e '\tDeleting | __blobstorage__'
find . -type d -name '__blobstorage__' -exec rm -r {} +

echo -e '\tDeleting | __queuestorage__'
find . -type d -name '__queuestorage__' -exec rm -r {} +

echo -e '\tDeleting | azurite artifacts'
find . -type f -name '__azurite*' -exec rm -r {} +

echo -e '\tDeleting | rider artifacts'
find . -type d -name '.idea' -exec rm -r {} +

echo -e '\tDeleting | logs'
find . -type f -name 'log-*.txt' -exec rm -r {} +

echo -e '\tDeleting | paket cache -- fake artifacts'
find . -type f -name 'paket-files/paket.restore.cached' -exec rm -r {} +

echo -e '\tDeleting | .fake'
find . -type d -name '.fake' -exec rm -r {} +

echo -e '\tDeleting | csv'
find . -type f -name '*.csv' -exec rm -r {} +
