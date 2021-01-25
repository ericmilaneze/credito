#!/bin/bash

dotnet tool install -g dotnet-reportgenerator-globaltool 2>/dev/null

rm -rf coveragereport
find ./tests -name TestResults -type d -exec rm -rf {} +

dotnet test --collect:"XPlat Code Coverage"

#[workaround] "dotnet test" 99% of the time makes terminal stop echoing
stty echo

reportgenerator \
    "-reports:tests/**/TestResults/**/coverage.cobertura.xml" \
    "-targetdir:coveragereport" \
    "-reporttypes:Html;HtmlChart"

find ./tests -name TestResults -type d -exec rm -rf {} +

xdg-open ./coveragereport/index.html & > /dev/null 2>&1

#[workaround] "xdg-open" always makes terminal stop echoing
stty echo
