#include <iostream>
#include <optional>
#include <chrono>
#include <functional>

template<
    typename T, 
    typename std::enable_if<
    std::is_integral<T>::value>::type* = nullptr, 
    uint32_t M = 66666>
T fast_modular_inverse(T x) {
    const T rest = x & ~1;
    T acc = 1;
    for(uint32_t i = 0; i < M; i++) {
        if(acc & (1 << i))
            acc -= rest << i;
    }
    const T mask = ~(~static_cast<T>(0) << M);
    return acc & mask; 
}

template<
    typename T, 
    typename std::enable_if<
    std::is_integral<T>::value>::type* = nullptr>
T gcd(T a, T b, T& x, T& y) {
    if (b == 0) {
        x = 1;
        y = 0;
        return a;
    }
    T x1, y1;
    T d = gcd(b, a % b, x1, y1);
    x = y1;
    y = x1 - y1 * (a / b);
    return d;
}

template<
    typename T, 
    typename std::enable_if<
    std::is_integral<T>::value>::type* = nullptr, 
    uint32_t M = 66666>
T naive_mod_inverse(T a) {
    T x, y;
    T g = gcd(a, M, x, y);
    if (g != 1)
        return -1;
    else
        return { (x%M + M) % M };
}

int main(int argc, char** argv) {
    uint64_t n;
    if(argc > 1) {
        n = std::strtoull(argv[1], NULL, 10);
    } else {
        return -1;
    }
    
    uint64_t ainv = fast_modular_inverse(n);
    std::chrono::high_resolution_clock::time_point t1_fast = 
        std::chrono::high_resolution_clock::now();
    volatile const auto _fast = fast_modular_inverse(n);
    const auto duration_fast = std::chrono::duration_cast<std::chrono::nanoseconds>(
        std::chrono::high_resolution_clock::now()-t1_fast).count();

    std::chrono::high_resolution_clock::time_point t1_slow = 
    std::chrono::high_resolution_clock::now();
    volatile const auto _slow = fast_modular_inverse(n);
    const auto duration_slow = std::chrono::duration_cast<std::chrono::nanoseconds>(
        std::chrono::high_resolution_clock::now()-t1_slow).count();    

    std::cout << duration_slow << " ns vs " << duration_fast << " ns" << std::endl;
    std::cout << _slow << std::endl;
    std::cout << _fast << std::endl;

}
