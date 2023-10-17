"use client"

import { cn } from "@/lib/utils"
import React from "react"
import ProjectCard from "./project-card"

import Link from "next/link"
import MainContainer from "@/components/main-container"
import { Button } from "@/components/ui/button"
import { Icons } from "@/components/icons"
import useProjects from "@/hooks/useProjects"

type Props = {}

const ProjectsPage = (props: Props) => {
  const { projects, isError } = useProjects()
 
  if(isError){
    return (<p>Something whent wrong try again!</p>
      )
  }

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
        {projects?.items &&
          projects.items.map((project) => (
            <ProjectCard
              key={project.id}
              projectId={project.id!}
              titleCard={project.name}
              descriptionCard=""
              bodyCard=""
              footerCard=""
            />
          ))}
      </div>
    </MainContainer>
  )
}

export default ProjectsPage
