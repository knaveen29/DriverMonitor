# ./Frontend/Dockerfile
FROM node:17-alpine  AS development

ENV NODE_ENV development

WORKDIR /app

COPY package*.json /app

RUN npm install

COPY . .

EXPOSE 3000

CMD ["npm", "start"]