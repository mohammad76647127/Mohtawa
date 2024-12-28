provider "aws" {
  region = "eu-north-1"  # Adjust to your AWS region
}

resource "aws_ecs_cluster" "example" {
  name = "my-cluster-name"
}

resource "aws_ecs_task_definition" "example" {
  family                   = "my-task"
  container_definitions    = jsonencode([
    {
      name        = "api-container"
      image       = "${var.ecr_registry}/${var.ecr_repository}:${var.image_tag}"
      essential   = true
      portMappings = [
        {
          containerPort = 80
          hostPort      = 80
        }
      ]
    }
  ])
  requires_compatibilities = ["FARGATE"]
  memory                   = "512"
  cpu                      = "256"
  network_mode             = "awsvpc"
}

resource "aws_ecs_service" "example" {
  name            = "my-service-name"
  cluster         = aws_ecs_cluster.example.id
  task_definition = aws_ecs_task_definition.example.arn
  desired_count   = 1
  launch_type     = "FARGATE"
}
