#!/bin/bash

ENV=$1

MAIN_CONFIG_FILE="./.depoy.cdk.aws-config"
ENV_CONFIG_FILE="./.depoy.cdk.aws-config-$ENV"
PLACEHOLDER_CONFIG_FILE="./.depoy.cdk.aws-config-ph"

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[0;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

if [ -f "$MAIN_CONFIG_FILE" ]; then
    echo "Reading main configuration file: $MAIN_CONFIG_FILE"

    . $MAIN_CONFIG_FILE

    else
        echo "${RED}Main configuration file does not exist: $MAIN_CONFIG_FILE${NC}"

        if [ -f "$PLACEHOLDER_CONFIG_FILE" ]; then
            cp $PLACEHOLDER_CONFIG_FILE $MAIN_CONFIG_FILE

            echo "${GREEN}A new main config file (${BLUE}$MAIN_CONFIG_FILE${GREEN}) was created using the placeholder file ($PLACEHOLDER_CONFIG_FILE)${NC}"
            echo "${YELLOW}Make the changes in ${BLUE}$MAIN_CONFIG_FILE${YELLOW} and then rerun this script.${NC}"
            
            exit;
        fi
fi

if [ -f "$ENV_CONFIG_FILE" ]; then
    echo "Reading the specific environment configuration file: $ENV_CONFIG_FILE"

    . $ENV_CONFIG_FILE

    else
        echo "${YELLOW}Specific environment configuration file was not found: ${BLUE}$ENV_CONFIG_FILE${NC}"
        echo "${GREEN}Creating environment configuration file: $ENV_CONFIG_FILE${NC}"

        touch $ENV_CONFIG_FILE
fi


rm -rf cdk.out

export CDK_DEPLOY_ENV=$ENV

if [ -z "$CDK_DEPLOY_AWS_PROFILE" ]; then
    echo "${YELLOW}AWS Profile not set. Default will be used.${NC}"

    cdk synth
    cdk deploy
    
    else
        echo "AWS Profile: $CDK_DEPLOY_AWS_PROFILE"

        cdk synth --profile $CDK_DEPLOY_AWS_PROFILE
        cdk deploy --profile $CDK_DEPLOY_AWS_PROFILE
fi

rm -rf cdk.out
