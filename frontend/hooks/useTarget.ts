import fetcher from "@/lib/fetcher"
import useSWR, { mutate } from "swr"
import { ITargetDto } from "@/lib/api/web-api-models"

export default function useTarget(projectId: string, targetId: string) {
  const url = `/backendapi/targets/${projectId}/${targetId}`

  const { data, error, isLoading } = useSWR<ITargetDto>(url, fetcher)

  const mutateTarget = async () => {
    mutate(url)
  }

  return {
    isLoading,
    isError: error,
    target: data,
    mutateTarget,
  }
}
