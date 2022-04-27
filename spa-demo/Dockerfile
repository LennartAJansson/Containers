# Stage 0, "build-stage", based on Node.js, to build and compile the frontend
FROM node:18.0 as build-stage
WORKDIR /app
COPY ./spa-demo/. .
RUN npm install
ARG configuration=production
RUN npm run build -- --output-path=./dist/out --configuration $configuration

# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:1.21.6
COPY --from=build-stage /app/dist/out/ /usr/share/nginx/html
COPY --from=build-stage /app/nginx-custom.conf /etc/nginx/conf.d/default.conf
