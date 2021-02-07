#!/bin/bash

ENV=$1


. ./.deploy.cdk.config.sh $ENV


rm -rf cdk.out


cdk destroy --profile $CDK_DEPLOY_AWS_PROFILE


rm -rf cdk.out
