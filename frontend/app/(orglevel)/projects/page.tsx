"use client"

import { cn } from "@/lib/utils";
import React, { useEffect, useState } from "react";
import ProjectCard from "./project-card";
import { ProjectHeadDto, ProjectsClient } from "@/lib/api/web-api-client";

import Link from "next/link";
import MainContainer from "../../components/main-container";
import { Button } from "../../components/ui/button";
import { Icons } from "../../components/icons";

type Props = {};

const ProjectsPage = (props: Props) => {
  const [projects, setProjects] = useState<ProjectHeadDto[]>([])

  useEffect(() => {
    (async () => {
      const client = new ProjectsClient('/backendapi')
      const response = await client.getList(1, 1000)
      setProjects(response.items!)
    })()
  }, [])

  return (
    <MainContainer props={{ className: "" }}>
      <div className={cn("")}>
        <Link href="/targets">
          <Button>
            <Icons.plus className="mr-2 h-4 w-4" />
            Add Project
          </Button>
        </Link>
      </div>
      <div className={cn("flex flex-wrap gap-4")}>
        {projects.map((project) => (
          <ProjectCard
            projectId={project.id!}
            titleCard={project.name}
            descriptionCard=""
            bodyCard=""
            footerCard=""
          />
        ))}
      </div>
    </MainContainer>
  );
};

export default ProjectsPage;
