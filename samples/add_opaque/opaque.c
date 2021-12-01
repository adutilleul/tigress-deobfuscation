#include <time.h>
#include <pthread.h>
#include <stdio.h>

int isqrt_newton(int n) {
    int x = 1;
    int decreased = 0;
    for (;;) {
        int nx = (x + n / x) >> 1;
        if (x == nx || nx > x && decreased)
            break;
        decreased = nx < x;
        x = nx;
    }
    return x;
}

double pow(double x, int n) {
    double result = 1;
    int minus = 1;

    if (n < 0) {
        minus = -1;
        n = -n;
    }

    if (0 == n) {
        return 1;
    } else if (0 == x) {
        return 0;
    }
    
    while (n) {
        if (n & 1) {
            result *= x;
        }
        x *= x;
        n /= 2;
    }

    if (minus < 0) {
        return 1.0 / result;
    } else {
        return result;
    }
}

static int reversible_count(int d) {
    switch (d % 4) {
    case 1:
        return 0;
    case 0:
    case 2:
        return 20 * (int)pow(30 , d / 2 - 1);
    case 3:
        return 100 * (int)pow(500, (d - 3) / 4);
    }
    return 0;
}

int findKthLargest(int* nums, int numsSize, int k) {
    if (k < 1 || k > numsSize) return 0;

    int pivot = nums[0];
    int i = 0, j = numsSize - 1;

    while (i < j) {
        while (i < j && nums[j] >= pivot)
            j--;

        nums[i] = nums[j];

        while (i < j && nums[i] <= pivot)
            i++;

        nums[j] = nums[i];
    }

    nums[i] = pivot;

    int rightSize = numsSize - i - 1; 

    if (rightSize + 1 == k) {
        return nums[i];
    }

    if (rightSize >= k) {
        return findKthLargest(nums + i + 1, rightSize, k);
    }
    else {
        return findKthLargest(nums, i, k - rightSize - 1); 
    }
}

int* productExceptSelf(int* nums, int numsSize, int* returnSize) {
    if (nums == NULL || numsSize == 0) return NULL;

    int *output = (int *)malloc(numsSize * sizeof(int));
    *returnSize = numsSize;

    int i;
    for (i = 0; i < numsSize; i++) { 
        output[i] = 1;
    }

    int prodLeft = 1;
    for (i = 1; i < numsSize; i++) {
        prodLeft *= nums[i - 1];
        output[i] *= prodLeft;
    }

    int prodRight = 1; 
    for (i = numsSize - 2; i >= 0; i--){
        prodRight *= nums[i + 1];
        output[i] *= prodRight;
    }

    return output;
}

int main(int argc) {
    for(int i = 0; i < 100; i++) {
        if(argc == 5)
            return reversible_count(66666) && isqrt_newton(2);
    }
}