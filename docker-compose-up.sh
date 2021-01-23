#!/bin/bash

env=$1

if [ -z "$env" ]
    then
        echo "Default configuration..."

        docker-compose -f docker-compose.yml up -d --build
else
        echo "$env configuration..."

        docker-compose -f docker-compose.yml -f docker-compose.$env.yml up -d --build
fi
