# ./Frontend/Dockerfile
FROM node:17-alpine  AS development

ENV NODE_ENV development
# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

WORKDIR /app

COPY package*.json ./

RUN npm install

RUN npm install react-scripts@5.0.1 -g --silent

COPY . ./

EXPOSE 3000

CMD ["npm", "start"]