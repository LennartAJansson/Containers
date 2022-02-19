kubectl delete -k ./deploy/apps
kubectl apply -k ./deploy/apps
#kubectl delete -k ./deploy/workloadsapi
#kubectl apply -k ./deploy/workloadsapi
#kubectl delete -k ./deploy/workloadsprojector
#kubectl apply -k ./deploy/workloadsprojector

curl.exe -X POST -g 'http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app=\"workloadsapi\"}'
curl.exe -X POST -g 'http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app=\"workloadsprojector\"}'
