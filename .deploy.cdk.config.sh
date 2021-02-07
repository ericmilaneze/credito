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

export CDK_DEPLOY_ENV=$ENV


MAIN_CONFIG_FILE="./.depoy.cdk.aws-config"
ENV_CONFIG_FILE="./.depoy.cdk.aws-config-$CDK_DEPLOY_ENV"
PLACEHOLDER_CONFIG_FILE="./.depoy.cdk.aws-config-ph"


if [ -z "$CDK_DEPLOY_NOT_LOCAL" ]; then
    echo "Deploying locally. Change the environment variable ${BLUE}\"CDK_DEPLOY_NOT_LOCAL\"${NC} to anything (true, 1, etc) when running this script not locally.${NC}"

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
    
    else
        echo "Deploying online. ${BLUE}\"CDK_DEPLOY_NOT_LOCAL\"${NC} set to ${BLUE}\"$CDK_DEPLOY_NOT_LOCAL\"${NC}"
fi


if [ -z "$CDK_DEPLOY_AWS_PROFILE" ]; then
    echo "${YELLOW}AWS Profile not set. Default will be used.${NC}"

    export CDK_DEPLOY_AWS_PROFILE=default
    
    else
        echo "AWS Profile: $CDK_DEPLOY_AWS_PROFILE"
fi
