import * as React from "react";

import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../../../components/ui/card";
import { Icons } from "../../../components/icons";
import { Button } from "../../../components/ui/button";

import MainContainer from "@/app/components/main-container";
import Link from "next/link";
import { cn } from "@/lib/utils";

const appDefinitions = [
  { id: 1, name: 'App Frontend', image: 'app-frontend', tag: 'latest', variableCount: 3, mountCount: 0 },
  { id: 2, name: 'App Backend', image: 'app-backend', tag: 'v1', variableCount: 2, mountCount: 2 },
  { id: 3, name: 'MySQL', image: 'mysql', tag: '8.0', variableCount: 4, mountCount: 1 },
  { id: 4, name: 'Redis Cache', image: 'redis', tag: 'latest', variableCount: 0, mountCount: 0 },
]

export default function AppDefinitionsPage() {
  return (
    <MainContainer props={{ className: "" }}>
      <div className={cn("")}>
        <Link href="/targets">
          <Button>
            <Icons.plus className="mr-2 h-4 w-4" /> Add App Definition
          </Button>
        </Link>
      </div>
      <div className="flex flex-wrap gap-4">
        {appDefinitions.map((appDefinition) => (
          <Card className="w-[350px]">
            <CardHeader>
              <CardTitle>{appDefinition.name}</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="flex w-full justify-between ">
                <div className="flex gap-1 ">
                  <span>Image:</span>
                  {appDefinition.image}
                </div>
                <div>
                  <span>Tag: {appDefinition.tag}</span>
                </div>
              </div>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button variant="ghost" className="text-sm font-bold">{appDefinition.variableCount} Variables</Button>
              <Button variant="ghost" className="text-sm font-bold">{appDefinition.mountCount} Mounts</Button>
            </CardFooter>
          </Card>
        ))}
      </div>
    </MainContainer>
  );
}
