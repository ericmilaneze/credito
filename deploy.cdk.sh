#!/bin/bash

ENV=$1


. ./.deploy.cdk.config.sh $ENV


rm -rf cdk.out


cdk bootstrap --profile $CDK_DEPLOY_AWS_PROFILE
cdk synth --profile $CDK_DEPLOY_AWS_PROFILE
cdk deploy --profile $CDK_DEPLOY_AWS_PROFILE


rm -rf cdk.out
