docker login -u softever -p softeverywhere docker.io && docker-compose -f docker-compose-dev.yml build crmrevo-project-api && docker-compose -f docker-compose-dev.yml push crmrevo-project-api