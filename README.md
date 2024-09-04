# Redis As Primary Database
POC Using Redis cach as primary database. 

## How to use this sample

- Step1: You need to install docker desktop and run docker image for redis. Below are docker compose file that i used.

```sh
version: '3.9'

services:
  cache:
    image: redis:latest
    restart: always    
    ports:
      - '6379:6379'
    command: redis-server --save 20 1 --loglevel warning --requirepass P@ssw0rd --user redis on >P@ssw0rd ~* allcommands --user default off nopass nocommands    
    volumes: 
      - cache:/data
volumes:
  cache:
    driver: local
```

- Step 2: Run docker compose up command to run redis image.

```sh
docker-compose up -d
```

_Make sure before your run docker compose command, You are in same path where you put your docker compose file_

Referance 

https://dotnetplaybook.com/redis-as-a-primary-database/