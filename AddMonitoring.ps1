# Setup needed deployments:
kubectl apply -k ./deploy/prometheus
kubectl apply -k ./deploy/grafana

# Using Helm to install loki
# helm repo add grafana https://grafana.github.io/helm-charts
# helm repo update
# helm upgrade -n monitoring --install loki grafana/loki
# helm uninstall loki
# kubectl apply -k Deploy/loki

"Add following to C:\Windows\System32\drivers\etc\hosts: "
"127.0.0.1 prometheus"
"127.0.0.1 prometheus.local"
"127.0.0.1 grafana"
"127.0.0.1 grafana.local"
"127.0.0.1 loki"
"127.0.0.1 loki.local"
""
"You should now be able to surf to:"
" http://prometheus.local:8081"
" http://grafana.local:8081/login"