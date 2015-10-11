#ifndef METHOD_POINTER_H
#define METHOD_POINTER_H
#include "methodPointable.h"

class genericMethodPointer {
    public:
        virtual ~genericMethodPointer() {}

    protected:
        class methodPointable *object;

        genericMethodPointer(class methodPointable *object) {
            this->object = object;
        }
};

template<typename in, typename out = void>
class methodPointer : public genericMethodPointer {
    public:
        methodPointer(out (methodPointable::*method)(in), class methodPointable *object) : genericMethodPointer(object) {
            this->method = method;
        }

        virtual ~methodPointer() {}

        out call(in a) {
            return object->*method(a);
        }

    private:
        out (methodPointable::*method)(in);
};

template<typename in1, typename in2, typename out = void>
class methodPointerDuo : public genericMethodPointer {
    public:
        methodPointerDuo(out (methodPointable::*method)(in1, in2), class methodPointable *object) : genericMethodPointer(object) {
            this->method = method;
        }

        virtual ~methodPointerDuo() {}

        out call(in1 a, in2 b) {
            return (object->*method)(a, b);
        }

    private:
        out (methodPointable::*method)(in1, in2);
};

#endif
