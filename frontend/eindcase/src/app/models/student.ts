

export interface Student{
    id?: number;
    name: string;
    lastName: string;
}

export const createStudent = (partialStudent: Partial<Student>): Student => {
    return {
        id: 0,
        name: '',
        lastName: '',
        ...partialStudent // spread
    };
};