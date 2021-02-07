#!/bin/bash

ENV=$1


. ./.deploy.cdk.config.sh $ENV


rm -rf cdk.out


cdk bootstrap --all --profile=$CDK_DEPLOY_AWS_PROFILE
cdk synth --all --profile=$CDK_DEPLOY_AWS_PROFILE
cdk deploy --all --require-approval=$CDK_DEPLOY_REQUIRE_APPROVAL --profile=$CDK_DEPLOY_AWS_PROFILE


rm -rf cdk.out
