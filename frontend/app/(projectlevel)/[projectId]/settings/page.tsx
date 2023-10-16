import ProjectCard from "@/app/(orglevel)/projects/project-card"
import MainContainer from "@/components/main-container"

const getProgect = async (id: number, token?: string) => {
  const data = await fetch(`https://api.kwikdeploy.com/projects/${id}`, {
    // in case we could retrieve the token from the session we could add it 
    // or fix the middlewere so when calling /backendapi/projects 
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
  const project = await data.json()
  return project
}
import { getSession } from "@/lib/getSession"
export default async function ProjectSettingsPage({
  params,
}: {
  params: { projectId: string }
}) {
  // const session = getSession()
  const project = await getProgect(parseInt(params.projectId))
  console.log(project)

  return (
    <MainContainer props={{ className: "" }}>
      <ProjectCard projectId={0} />
    </MainContainer>
  )
}
