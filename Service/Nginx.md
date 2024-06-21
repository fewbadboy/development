# document

```conf
# nginx.conf

http {
    # 负载均衡 加权轮询
    upstream backend {
        server 127.0.0.1 weight=3;
        server 127.0.0.2 weight=2;
        server 127.0.0.3 weight=1;
    }

    server {
        listen 443 ssl; # 启用 TLS
        server_name localhost;
        ssl_certificate  /usr/local/nginx/conf/cert/server.pem;
        ssl_certificate_key /usr/local/nginx/conf/cert/server-key.pem;

        location / {
            root html/web;
            index index.html;
        }

        location /backend {
             proxy_pass http://backend;
        }

        # proxy wss
        location /mqtt {
            proxy_pass http://127.0.0.1:8000;
            proxy_http_version 1.1;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection $connection_upgrade;
            # proxy_set_header Host $host;
            # proxy_set_header X-Real-IP $remote_addr;
            # proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            # proxy_set_header X-Forwarded-Proto $scheme;
        }

        error_page 500 501 /50x.html;
    }
}
```
