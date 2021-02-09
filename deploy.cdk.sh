#!/bin/bash

ENV=$1


. ./.deploy.cdk.config.sh $ENV


cdk deploy \
    --require-approval=$CDK_DEPLOY_REQUIRE_APPROVAL \
    --profile=$CDK_DEPLOY_AWS_PROFILE \
    -c CDK_DEPLOY_ENV=$CDK_DEPLOY_ENV \
    -c CDK_DEPLOY_AWS_ACCOUNT=$CDK_DEPLOY_AWS_ACCOUNT \
    -c CDK_DEPLOY_AWS_REGION=$CDK_DEPLOY_AWS_REGION \
    -c CDK_DEPLOY_VPC_ID=$CDK_DEPLOY_VPC_ID \
    --all


rm -rf cdk.out
