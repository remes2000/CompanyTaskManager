import { Workplacement } from './Workplacement';
import { User } from './User'

export interface Task{
    taskId: number
    title: string,
    description: string,
    priority: string, 
    employeeId: number,
    employee: User,
    addedById: number,
    addedBy: User,
    workplacementId: number,
    workplacement: Workplacement,
    addDate: Date
    status: string
}