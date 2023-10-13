import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { cn } from "@/lib/utils";
import Link from "next/link";

type CardAttribute = {
  projectId: number;
  titleCard?: string;
  descriptionCard?: string;
  bodyCard?: string;
  footerCard?: string;
};

type CardProps = React.ComponentProps<typeof Card> & CardAttribute;

const ProjectCard = ({
  className,
  projectId,
  titleCard,
  descriptionCard,
  bodyCard,
  footerCard,
  ...props
}: CardProps) => {
  return (
    <Link href={`/${projectId}/targets`}>
      <Card className={cn("w-[304px] ", className)} {...props}>
        <CardHeader>
          <CardTitle>{titleCard}</CardTitle>
          <CardDescription>{descriptionCard} </CardDescription>
        </CardHeader>
        <CardContent className="grid gap-4">
          <div>{bodyCard}</div>
        </CardContent>
        <CardFooter>{footerCard}</CardFooter>
      </Card>
    </Link>
  );
};

export default ProjectCard;
