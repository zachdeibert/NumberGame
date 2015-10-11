#include "methodPointer.h"

genericMethodPointer::genericMethodPointer(class methodPointable *object) {
    this->object = object;
}

genericMethodPointer::~genericMethodPointer() {}

template<typename in, typename out>
methodPointer<in, out>::methodPointer(out (methodPointable::*method)(in), methodPointable *object) : genericMethodPointer(object) {
    this->method = method;
}

template<typename in, typename out>
methodPointer<in, out>::~methodPointer() {}

template<typename in, typename out>
out methodPointer<in, out>::call(in a) {
    return (*object.*method)(a);
}

template<typename in1, typename in2, typename out>
methodPointerDuo<in1, in2, out>::methodPointerDuo(out (methodPointable::*method)(in1, in2), methodPointable *object) : genericMethodPointer(object) {
    this->method = method;
}

template<typename in1, typename in2, typename out>
methodPointerDuo<in1, in2, out>::~methodPointerDuo() {}

template<typename in1, typename in2, typename out>
out methodPointerDuo<in1, in2, out>::call(in1 a, in2 b) {
    return (*object.*method)(a, b);
}
