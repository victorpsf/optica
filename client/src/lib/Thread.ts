
export const sleep = (time: number): Promise<boolean> => new Promise((resolve: (value: boolean) => void) => {
    time *= 1000;

    setTimeout(() => resolve(true), time);
})