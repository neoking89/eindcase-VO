import { Student } from "./student";

export interface Employee extends Student{
    companyName: string;
    department: string;
    quoteNumber: number;
}

