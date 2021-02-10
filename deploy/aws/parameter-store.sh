#!/bin/bash

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[0;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color


ENV=$1

if [ -z "$ENV" ]; then
    echo "${YELLOW}Environment not set. Default (\"dev\") will be used.${NC}"
    ENV=dev

    else
        echo "Environment: $ENV"
fi


ENV_CONFIG_FILE="./.parameter-store-$ENV"
PLACEHOLDER_CONFIG_FILE="./.parameter-store-ph"


if [ -f "$ENV_CONFIG_FILE" ]; then
    echo "Reading the specific environment configuration file: $ENV_CONFIG_FILE"

    . $ENV_CONFIG_FILE

    else
        echo "${RED}Specific environment configuration file was not found: ${BLUE}$ENV_CONFIG_FILE${NC}"

        cp $ENV_CONFIG_FILE $PLACEHOLDER_CONFIG_FILE

        echo "${RED}Specific environment configuration file was created and MUST be changed for this script to work: ${BLUE}$ENV_CONFIG_FILE${NC}"

        exit;
fi


if [ -z "$AWS_PROFILE" ]; then
    echo "${RED}AWS Profile not set. Set it and rerun this script.${NC}"

    exit;
    
    else
        echo "AWS Profile: $AWS_PROFILE"
fi


aws ssm put-parameter \
    --name /$ENV/credito/web-api/db/connection-string \
    --value "$CREDITO_DB_CONNECTION_STRING" \
    --type String \
    --profile $AWS_PROFILE \
    --overwrite

aws ssm put-parameter \
    --name /$ENV/credito/web-api/db/database-name \
    --value "$CREDITO_DB_DATABASE_NAME" \
    --type String \
    --profile $AWS_PROFILE \
    --overwrite
