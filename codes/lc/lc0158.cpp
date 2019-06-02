#include "pch.h"

/*
 * tags: design
 * Time(n), Space(1)
 */

// Forward declaration of the read4 API.
int read4(char *buf);

class lc0158_Solution {
public:
	lc0158_Solution() : _cnt(0), _pos(0) {}
    
    /**
     * @param buf Destination buffer
     * @param n   Number of characters to read
     * @return    The number of actual characters read
     */
    int read(char *buf, int n) {
        int r = 0;
        while (r < n) {
            if (_pos == 0) _cnt = read4(_buf);
            if (_cnt == 0) break;
            while (r < n && _pos < _cnt) 
                buf[r++] = _buf[_pos++];
            if (_pos == _cnt) _pos = 0;
        }
        
        return r;
    }
    
private:
    char _buf[4];
    int _cnt, _pos;
};
