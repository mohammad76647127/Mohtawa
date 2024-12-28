variable "allocated_storage" {}
variable "engine" {}
variable "instance_class" {}
variable "name" {}
variable "username" {}
variable "password" { sensitive = true }
variable "parameter_group_name" {}
variable "subnet_group_name" {}
variable "tags" { type = map(string) }
