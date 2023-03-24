import { setDuration } from './helper-functions';

describe('setDuration', () => {
  it('calculates duration in days', () => {
    const startDate = new Date('2022-03-01');
    const endDate = new Date('2022-03-10');
    const expectedDuration = 9;

    const actualDuration = setDuration(startDate, endDate);

    expect(actualDuration).toBe(expectedDuration);
  });

});
