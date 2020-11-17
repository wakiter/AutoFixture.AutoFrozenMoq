#!/bin/bash
set -e

version=`cat ./version`

echo "Current version is: ${version}."

major=0
minor=0
build=0

# break down the version number into it's components
regex="([0-9]+).([0-9]+).([0-9]+)"
if [[ $version =~ $regex ]]; then
  major="${BASH_REMATCH[1]}"
  minor="${BASH_REMATCH[2]}"
  build="${BASH_REMATCH[3]}"
fi

build=$((build + 1))
newVersion="${major}.${minor}.${build}"

echo "New version: ${newVersion}"

echo "${major}.${minor}.${build}" > ./version

export NEWVERSION=$newVersion


