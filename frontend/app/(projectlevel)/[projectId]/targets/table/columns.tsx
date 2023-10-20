"use client"

import { ColumnDef } from "@tanstack/react-table"
import { ITargetHeadDto } from "@/lib/api/web-api-models"

export const columns: ColumnDef<ITargetHeadDto>[] = [
  {
    accessorKey: "id",
  },
  {
    accessorKey: "name",
    header: "Name",
  },
  {
    accessorKey: "connected",
    header: "Connected",
  },
]
